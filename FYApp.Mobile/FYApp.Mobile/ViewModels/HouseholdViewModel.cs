using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using FYApp.Mobile.Models;
using FYApp.Mobile.Views;
using FYApp.Core.Models;

namespace FYApp.Mobile.ViewModels
{
    public class HouseholdViewModel : BaseViewModel
    {
        public ObservableCollection<Household> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public HouseholdViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Household>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Household>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Household;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}