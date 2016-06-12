using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using SplitterBox.Annotations;

namespace SplitterBox
{
    public class ViewModel : INotifyPropertyChanged
    {
        private readonly ICollectionView collectionView;
        private string filterText;

        public ViewModel()
        {
            for (int i = 0; i < 100; i++)
            {
                Items.Add("isak");
                Items.Add("reed");
                Items.Add("dk");
            }

            collectionView = CollectionViewSource.GetDefaultView(Items);
            collectionView.Filter = IsMatch;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string FilterText
        {
            get { return filterText; }
            set
            {
                if (value == filterText) return;
                filterText = value;
                OnPropertyChanged();
                collectionView.Refresh();
            }
        }

        public ObservableCollection<string> Items { get; } = new ObservableCollection<string>();

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool IsMatch(object obj)
        {
            if (string.IsNullOrEmpty(FilterText))
            {
                return true;
            }

            var text = (string)obj;
            return text.Contains(this.FilterText);
        }
    }
}
