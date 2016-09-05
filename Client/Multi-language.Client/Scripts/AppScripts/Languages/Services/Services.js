
appLanguages.service("SinglePageCRUDService", function ($http) {

    //Function to Read All Employees
    this.getLanguages = function () {
        return $http.get("http://localhost:4446/api/Languages");
    };

    //Fundction to Read Language based upon id
    this.getLanguage = function (id) {
        return $http.get("http://localhost:4446/api/Languages/" + id);
    };

    //Function to create new Language
    this.post = function (Language) {
        var request = $http({
            method: "post",
            url: "http://localhost:4446/api/Languages",
            data: Language
        });
        return request;
    };
    //Function  to Edit Language based upon id 
    this.put = function (id,Language) {
        var request = $http({
            method: "put",
            url: "http://localhost:4446/api/Languages/" + id,
            data: Language
        });
        return request;
    };

    //Function to Delete Language based upon id
    this.delete = function (id) {
        var request = $http({
            method: "delete",
            url: "http://localhost:4446/api/Languages/" + id
        });
        return request;
    };
});




