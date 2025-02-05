using CinemaTiketsShop.Data.Services;
using CinemaTiketsShop.ViewModels.ActorVMs;



namespace CinemaTiketsShop.Dictionarys
{
    public class ModelDictionary
    {
        private readonly IActorServices? _ActorService;
        private readonly IProducerService? _ProducerService;
        private readonly ICinemaService? _CinemaService;

        public ModelDictionary(IActorServices? ActorService = null, IProducerService? ProducerService = null, ICinemaService? CinemaService = null)
        {
            _ActorService = ActorService;
            _ProducerService = ProducerService;
            _CinemaService = CinemaService;
        }


        public async Task<IDictionary<int, ActorSelectorView>> SelectActorsKeysAndNames() 
        {
            if(_ActorService == null) 
            {
                return new Dictionary<int, ActorSelectorView>();
            }

            var ActorDictionary = new Dictionary<int, ActorSelectorView>();

            var ActorsList = await _ActorService.GetActors();

            foreach (var Actor in ActorsList) 
            {
                ActorDictionary[Actor.Id] = new ActorSelectorView
                {
                    Id = Actor.Id,
                    Name = Actor.Name,
                    Picture = Actor.FotoURL
                };
            }

            return ActorDictionary;
        }

        public async Task<IDictionary<int, string>> SelectProducersKeysAndNames()
        {
            if (_ProducerService == null)
            {
                return new Dictionary<int, string>();
            }

            var ProducerDictionary = new Dictionary<int, string>();

            var ProducerList = await _ProducerService.GetAll();

            foreach (var p in ProducerList)
            {
                ProducerDictionary[p.Id] = p.Name;
            }

            return ProducerDictionary;
        }

        public async Task<IDictionary<int, string>> SelectCinemasKeysAndNames()
        {
            if (_CinemaService == null)
            {
                return new Dictionary<int, string>();
            }

            var CinemaDictionary = new Dictionary<int, string>();

            var CinemaList = await _CinemaService.GetAll();

            foreach (var c in CinemaList)
            {
                CinemaDictionary[c.Id] = c.Name;
            }

            return CinemaDictionary;
        }
    }
}
