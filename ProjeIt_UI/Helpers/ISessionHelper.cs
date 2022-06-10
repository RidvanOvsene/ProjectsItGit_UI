namespace ProjeIt_UI.Helpers
{
    public interface ISessionHelper
    {
        void SetSeesionModel(object SeesionModel, string SessionName);
        string GetSessionModel(string SessionKey);
    }
}
