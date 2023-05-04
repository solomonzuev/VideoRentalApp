using System;
using System.Windows.Controls;

namespace VideoRental.Domain
{
    public class MenuItemViewModel : ViewModelBase
    {
        private readonly string _title;
        private Page? _page;
        private readonly Type _contentType;

        public string Title => _title;

        public Page Page
        {
            get => _page ??= CreateContent();
            set => SetProperty(ref _page, value);
        }

        private Page CreateContent()
        {
            var content = Activator.CreateInstance(_contentType);

            if (content is Page _page)
            {
                return _page;
            }

            throw new ArgumentException($"Failed to create a page instance. Invalid content type");
        }

        public MenuItemViewModel(string title, Type contentType)
        {
            _title = title;
            _contentType = contentType;
        }
    }
}
