using System.Runtime.CompilerServices;
using AutoMapper;
using WebApi.BLL.Logics.Interfaces;
using WebApi.DAL.Repositories.Interfaces;

namespace WebApi.BLL.Logics
{
    public abstract class BaseLogic:IBaseLogic
    {
        public readonly IUnitOfWork _unitOfWork;
        public IMapper _mapper;
        public BaseLogic(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public IUnitOfWork unitOfWork
        {
            get
            {
                return _unitOfWork;
            }
        }

        public IMapper mapper
        {
            get
            {
                return _mapper;
            }
        }
    }
}
