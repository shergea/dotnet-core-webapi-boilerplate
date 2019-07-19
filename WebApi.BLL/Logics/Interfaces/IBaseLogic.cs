using AutoMapper;
using WebApi.DAL.Repositories.Interfaces;

namespace WebApi.BLL.Logics.Interfaces
{
    public interface IBaseLogic
    {
        IUnitOfWork unitOfWork{get;}
        IMapper mapper{get;}
    }
}