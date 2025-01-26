using CinemaTiketsShop.Models;

namespace CinemaTiketsShop.Data.Wrappers
{
    public class ProducerResult
    {
        public Producer? Producer { get; }
        public bool isFound { get; }
        public bool UpdateSucceded { get; }

        public ProducerResult(Producer? producer = null, bool isFound = false, bool isUpdated  = false)
        {
            this.isFound = isFound;
            this.UpdateSucceded = isUpdated;
            this.Producer = producer;
        }

        public static ProducerResult Found(Producer producer) 
        {
            return new ProducerResult(producer, true);
        }

        public static ProducerResult NotFound()
        {
            return new ProducerResult();
        }

        public static ProducerResult UpdateSuccess(Producer producer) 
        {
            return new ProducerResult(producer, true, true);
        }

        public static ProducerResult UpdateFails()
        {
            return new ProducerResult();
        }
    }
}
