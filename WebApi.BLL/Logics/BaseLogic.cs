using System.Runtime.CompilerServices;
using WebApi.BLL.Logics.Interfaces;
using WebApi.DAL.Repositories.Interfaces;

namespace WebApi.BLL.Logics
{
    public abstract class BaseLogic:IBaseLogic
    {
        public readonly IUnitOfWork _unitOfWork;
        public BaseLogic(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public IUnitOfWork unitOfWork
        {
            get
            {
                return _unitOfWork;
            }
            set{}
        }
    }
}
