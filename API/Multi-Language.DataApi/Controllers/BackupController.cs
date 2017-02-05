using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Multi_language.Services;
using System.Threading.Tasks;
using System.Web.Configuration;
using Multi_language.Common;
using Multi_language.Common.Enums;
using Multi_language.Common.Helpers;
using Multi_Language.DataApi.Models;

namespace Multi_Language.DataApi.Controllers
{
    [RoutePrefix("backup")]
    public class BackupController : ApiController
    {
        private readonly IBackupService backupService;
        private string backupFolder = "~/DBBackups";
        private static string backupFilePath;
        private readonly string dbName = WebConfigurationManager.AppSettings["DatabaseName"];

        public BackupController(IBackupService backupService)
        {
            this.backupService = backupService;
            this.backupService.Initialiaze(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, dbName);
            backupFilePath = HttpContext.Current.Server.MapPath(backupFolder);
        }

        [Route("getall", Name = "GetBackupFileList")]
        public IEnumerable<FileDataModel> GetBackupList()
        {
            if (User != null)
            {
                var userClaims = User.Identity.AuthenticationType;
                //TODO Should be logged in
            }
            return Get(backupFolder, "bak", name =>
                    new FileDataModel(name, Utils.GetFileSizeString(Path.Combine(backupFilePath, name))));
        }

        [Route("getpath", Name = "GetBackupPath")]
        public IHttpActionResult GetBackupPath()
        {
            return Ok(HttpContext.Current.Server.MapPath(backupFolder).ToString());
        }

        private static IEnumerable<T> Get<T>(string folderPath, string requiredExtension, Func<string, T> parser) where T : class
        {
            try
            {
                if (!Directory.Exists(backupFilePath))
                {
                    Directory.CreateDirectory(backupFilePath);
                }

                var files = GetFiles(backupFilePath, requiredExtension);
                return files.Select(_ => parser(_.Name));
            }
            catch (Exception)
            {
                // TODO: handle better (notify caller?) - or at least Log?
                return Enumerable.Empty<T>();
            }
        }

        /// <summary>
        /// Create a new backup from the current configuration.
        /// </summary>
        /// <returns></returns>
        [AuthorizeEnum(ERoleLevels.AdminPermissions)]
        [Route("create/{fileSuffix?}", Name = "CreateBackup")]
        public IHttpActionResult CreateBackup(string fileSuffix = "")
        {
            try
            {
                var filePath = RequestBackupFile(fileSuffix);
                return Ok(filePath);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        private string RequestBackupFile(string fileSuffix)
        {
            var hostname = Dns.GetHostName();
            var version = "1.0.0.0";

            return backupService.Backup(backupFilePath, hostname, version, fileSuffix);
        }


        /// <summary>
        /// Upload a new backup.
        /// </summary>
        /// <returns></returns>
        //TODO move to FileController... + handle filename exists...
        [Route("upload", Name = "UploadBackup")]
        [AuthorizeEnum(ERoleLevels.BackupPermissions, ERoleLevels.AdminPermissions)]
        [HttpPost]
        public async Task<IHttpActionResult> UploadBackup()
        {
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);
            //TODO get all uploaded files
            var fileName = provider.Contents[0].Headers.ContentDisposition.FileName.Trim('\"');
            var buffer = await provider.Contents[0].ReadAsByteArrayAsync();
            if (fileName.EndsWith("bak"))
            {
                string filePath = Path.Combine(backupFilePath, fileName);
                //TODO: check if there is not yet a file with same name...
                try
                {
                    File.WriteAllBytes(filePath, buffer);

                    return Ok("Backup successfully uploaded");
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
            return BadRequest("Error in uploading file. Not correct extension.");
        }

        /// <summary>
        /// Restores the data from the backup with the specified filename.
        /// </summary>
        /// <param name="filename">The filename of the backup that should be deleted.</param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeEnum(ERoleLevels.BackupPermissions, ERoleLevels.AdminPermissions)]
        [Route("restore/{filename}", Name = "RestoreBackup")]
        public IHttpActionResult RestoreBackup(string filename)
        {
            var serverPath = Path.Combine(backupFilePath, filename);
            var fileInfo = new FileInfo(serverPath);

            if (!fileInfo.Exists)
            {
                return NotFound();
            }

            backupService.Restore(serverPath, dbName, dbName + "_log");
            return Ok("Database is restored successfully.");
        }

        [Route("delete/{filename}", Name = "DeleteBackupFile")]
        [AuthorizeEnum(ERoleLevels.AdminPermissions)]
        [HttpPost]
        public IHttpActionResult DeleteBackup(string filename)
        {
            return DeleteFile(backupFilePath, filename);
        }

        private IHttpActionResult DeleteFile(string folderPath, string filename)
        {
            // Get file from config
            var filePath = Path.Combine(folderPath, filename);
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                // Nothing to do...
                return NotFound();
            }
            try
            {
                File.Delete(filePath);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
            return Ok("ok");
        }

        /// <summary>
        /// Get the list of files in the specified path with the specified extension.
        /// Extension is without '.'
        /// </summary>
        /// <param name="folderPath">The path of the folder for which we want the files</param>
        /// <param name="extension">The extension of the files (without '.').</param>
        /// <returns>List of files in the specified folder.</returns>
        private static IList<FileInfo> GetFiles(string folderPath, string extension)
        {
            var directory = new DirectoryInfo(folderPath);
            var files = directory.GetFiles();
            files = files.OrderBy(f => f.Name).ToArray();
            return files.Where(f => f.Extension == $".{extension}").ToList();
        }

        /// <summary>
        /// Download the backup with the specified name.
        /// </summary>
        /// <param name="filename">The filename of the backup that should be downloaded.</param>
        /// <returns></returns>
        [Route("download/{filename}", Name = "GetBackupFile")]
        public HttpResponseMessage GetBackup(string filename)
        {
            return Download(backupFilePath, filename, "bak");
        }

        private HttpResponseMessage Download(string folderPath, string filename, string requiredExtension)
        {
            var minLength = requiredExtension.Length + 2;
            if (string.IsNullOrWhiteSpace(filename) || filename.Length < minLength || !filename.EndsWith($".{requiredExtension}"))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid file extension.");
            }

            var serverPath = Path.Combine(folderPath, filename);
            var fileInfo = new FileInfo(serverPath);

            if (fileInfo.Exists)
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new ByteArrayContent(File.ReadAllBytes(fileInfo.FullName));
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response.Content.Headers.ContentDisposition.FileName = fileInfo.Name;

                return response;
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "File not found.");
        }

    }
}

