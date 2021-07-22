using Core.Entities.Concrete;

namespace Business.Constants
{
    public static class Messages
    {
        public static string GetAllVideos = "All videos fetched.";
        public static string GetVideo = "Video fetched.";
        public static string VideoAdded = "Video added.";
        public static string VideoDeleted = "Video deleted.";
        public static string VideoUpdated = "Video updated.";
        public static string VideoUploadedToHost = "Video uploaded to host.";
        public static string VideoDeletedFromHost = "Video deleted from host.";
        public static string NotAVideo = "This is not a supported type of video, upload .mp4, .avi, .wav";
        public static string UserRegistered = "User registered.";
        public static string UserNotFound = "User not found.";
        public static string PasswordError = "Wrong password.";
        public static string SuccessfulLogin = "Logged in succesfuly";
        public static string UserAlreadyExist = "User already exists.";
        public static string AccessTokenCreated = "Token created.";
        public static string AuthorizationDenied = "You dont have permisson.";
    }
}