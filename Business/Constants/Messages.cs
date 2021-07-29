using Core.Entities.Concrete;

namespace Business.Constants
{
    public static class Messages
    {
        public static string GetVideos = "Videos fetched.";
        public static string GetVideo = "Video fetched.";
        public static string VideoAdded = "Video added.";
        public static string VideoDeleted = "Video deleted.";
        public static string VideoUpdated = "Video updated.";
        public static string VideoUploadedToHost = "Video uploaded to host.";
        public static string VideoDeletedFromHost = "Video deleted from host.";
        public static string NotAVideo = "This is not a supported type of video, try with .mp4, .avi, .wav";
        public static string UserRegistered = "User registered.";
        public static string UserNotFound = "User not found.";
        public static string PasswordError = "Wrong password.";
        public static string SuccessfulLogin = "Logged in.";
        public static string UserAlreadyExist = "User already exists.";
        public static string AccessTokenCreated = "Token created.";
        public static string AuthorizationDenied = "You dont have permission.";
        public static string PlaylistAdded = "Playlist added.";
        public static string PlaylistDeleted = "Playlist deleted.";
        public static string PlaylistUpdated = "Playlist updated.";
        public static string GetPlaylist = "Playlist fetched.";
        public static string GetPlaylists = "Playlists fetched.";
        public static string UserAdded = "User added.";
        public static string UserUpdated = "User updated.";
        public static string UserDeleted = "User deleted.";
        public static string GetUser = "User fetched.";

        public static string ValidationFailed = "Validation failed";
    }
}