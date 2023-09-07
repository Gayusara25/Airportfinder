using Airportfinder.Models;
using Airportfinder.RepositoryPattern;

namespace Airportfinder.Services.Implementation
{
    public class StateImgService : IStateImg
    {
        private readonly IRepository<StateImg> _stateImgRepository;
        public StateImgService(IRepository<StateImg> stateImgRepository)
        {

            _stateImgRepository = stateImgRepository;
        }
        public List<StateImg> GetStateImgList()
        {

            return _stateImgRepository.Get().ToList();
        }
    }
}
