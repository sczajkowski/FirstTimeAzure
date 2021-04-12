using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace xaml_api_db
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            btnPhoto.Clicked += btnPhoto_Clicked;
        }

        private async void btnPhoto_Clicked(object sender, EventArgs e)
        {
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
            });
            if (photo == null)
                return;


            imgPhoto.Source = ImageSource.FromStream(() => photo.GetStream());
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                photo.GetStream().CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }

            string base64 = Convert.ToBase64String(bytes);
            //person.PictureBase64 = base64;
        }


    }
}
