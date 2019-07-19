using WebApi.Model;
using WebApi.Model.ViewModels.AuthController;

namespace WebApi.BLL.Logics.Interfaces
{
    public interface IAuthLogic
    {
        User Authenticate(PostLoginInputViewModel entity);
    }
}