using System.Collections.ObjectModel;

namespace Server
{
    class LimitedObservableQueue<T> : ObservableCollection<T>
    {
        private ObservableCollection<T> _oc = new ObservableCollection<T>();
        public int Limit { get; set; }

        public LimitedObservableQueue(int limit)
        {
            this.Limit = limit;
        }
    }
}
