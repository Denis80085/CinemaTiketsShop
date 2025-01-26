using CinemaTiketsShop.Models;

namespace CinemaTiketsShop.Data.Wrappers
{
    public class ActorResult
    {
        public bool _isFound { get; }
        public Actor? _actor { get; }

        public ActorResult(bool isFound, Actor? actor)
        {
            _isFound = isFound;
            _actor = actor;
        }

        public static ActorResult Found(Actor actor) 
        {
            return new ActorResult(true, actor);
        }

        public static ActorResult NotFound() 
        {
            return new ActorResult(false, null);
        }
    }
}
