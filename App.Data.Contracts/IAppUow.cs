namespace App.Data.Contracts
{
    public interface IAppUow
    {
        void Commit();


        IUserProfileRepository UserProfiles { get; }
        IVideoRepository Videos { get; }
        IPlaylistRepository Playlists{ get; }
    }
}
