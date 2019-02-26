using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace BindingIssue.ViewModels
{
    public class PostCellViewModel: INotifyPropertyChanged
    {
        public string Author { get; set; }
        public string ProfilePicUrl { get; set; }
        public string Text { get; set; }
        public DateTime Timestamp { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private Services.Result _user;

        private readonly Services.RandomMeService _userService;

        public PostCellViewModel(Services.RandomMeService service)
        {
            _userService = service;
        }

        public async Task OnAppearing()
        {
            _user = await _userService.CreateUser();
            var text = "Blah 1 Blah 2 Blah 3 Blah 4 Blah 5 Blah 6 Blah 7 Blah 8 Blah 9 Blah 10 Blah 11 Blah 12";

            this.Text = text;
            this.Author = _user.name.first;
            this.ProfilePicUrl = _user.picture.medium;
            this.Timestamp = DateTime.Now;

            OnPropertyChanged(nameof(ProfilePicUrl));
            OnPropertyChanged(nameof(Text));
            OnPropertyChanged(nameof(Author));
            OnPropertyChanged(nameof(Timestamp));

        }

    }
}
