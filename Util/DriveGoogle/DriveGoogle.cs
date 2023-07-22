using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using static Google.Apis.Drive.v3.DriveService;

namespace DeZooiNaCrypto.Util.DriveGoogle
{
    public class DriveGoogle
    {
        private DriveService GetService()
        {
            var tokenResponse = new TokenResponse
            {
                AccessToken = "ya29.a0AbVbY6M91wCvFt78aIMRal2juVzhLHp9e-y-89d64IrTGYkQXUm1wJxJK8b_YZMNKBa5us0-df3RhbxVcOeB770kOT_hU8oFq8C6XIjvyNMs00WGYSuHhEEit9dQxfHTHDOZW4a8SZdKl44Wy3SoZ5IA_mSAaCgYKAVwSARESFQFWKvPlbaWsxa9I5TkT7B1fYy72VQ0163",
                RefreshToken = "1//04TZW3UR8MWDCCgYIARAAGAQSNwF-L9IrsQGzLgetL9Zc7D9daOsYPGuTCLsbxN2wCgkCv3KTBo0zKxPQjLi8LYyal9NVlsb9A-o",
            };


            var applicationName = "DeZooiNaCrypto";// Use the name of the project in Google Cloud
            var username = "mvthomazoliveira@gmail.com";// Use your email


            var apiCodeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = "434518053435-cp342n6i76e3151lnt01q0gk2rk9mbeo.apps.googleusercontent.com",
                    ClientSecret = "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"
                },
                Scopes = new[] { Scope.Drive },
                DataStore = new FileDataStore(applicationName)
            });


            var credential = new UserCredential(apiCodeFlow, username, tokenResponse);


            var service = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName
            });

            return service;
        }
        public string CreateFolder(string parent, string folderName)
        {
            var service = GetService();
            var driveFolder = new Google.Apis.Drive.v3.Data.File();
            driveFolder.Name = folderName;
            driveFolder.MimeType = "application/vnd.google-apps.folder";
            driveFolder.Parents = new string[] { parent };
            var command = service.Files.Create(driveFolder);
            var file = command.Execute();
            return file.Id;
        }
        public string UploadFile(Stream file, string fileName, string fileMime, string folder, string fileDescription)
        {
            DriveService service = GetService();


            var driveFile = new Google.Apis.Drive.v3.Data.File();
            driveFile.Name = fileName;
            driveFile.Description = fileDescription;
            driveFile.MimeType = fileMime;
            driveFile.Parents = new string[] { folder };


            var request = service.Files.Create(driveFile, file, fileMime);
            request.Fields = "id";

            var response = request.Upload();
            if (response.Status != Google.Apis.Upload.UploadStatus.Completed)
                throw response.Exception;

            return request.ResponseBody.Id;
        }
        public void DeleteFile(string fileId)
        {
            var service = GetService();
            var command = service.Files.Delete(fileId);
            var result = command.Execute();
        }
        public IEnumerable<Google.Apis.Drive.v3.Data.File> GetFiles(string folder)
        {
            var service = GetService();

            var fileList = service.Files.List();
            fileList.Q = $"mimeType!='application/vnd.google-apps.folder' and '{folder}' in parents";
            fileList.Fields = "nextPageToken, files(id, name, size, mimeType)";

            var result = new List<Google.Apis.Drive.v3.Data.File>();
            string pageToken = null;
            do
            {
                fileList.PageToken = pageToken;
                var filesResult = fileList.Execute();
                var files = filesResult.Files;
                pageToken = filesResult.NextPageToken;
                result.AddRange(files);
            } while (pageToken != null);


            return result;
        }
    }
}
