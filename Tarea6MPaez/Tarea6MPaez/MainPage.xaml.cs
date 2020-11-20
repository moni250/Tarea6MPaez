using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Tarea6MPaez
{

    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private const string Url = "http://192.168.1.13/moviles/post.php";
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<Tarea6MPaez.WS.Estudiante> _post;
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnConsultar_Clicked(object sender, EventArgs e)
        {
            var content = await client.GetStringAsync(Url);
            List<Tarea6MPaez.WS.Estudiante> posts = JsonConvert.DeserializeObject<List<Tarea6MPaez.WS.Estudiante>>(content);
            _post = new ObservableCollection<WS.Estudiante>(posts);

            MyListView.ItemsSource = _post;

            await DisplayAlert("Alerta", "Para eliminar o editar seleccione un item para acceder a la pantalla de edicion", "ok");
        }

        private async void btnVistaInsertar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new vistaInsertar());
        }
        private async void btnVistaEditar_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new vistaEditar("","","",""));
        }
        private async void MyListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (Tarea6MPaez.WS.Estudiante)e.SelectedItem;
            var id = Convert.ToString(item.codigo);
            var nombre = item.nombre;
            var apellido = item.apellido;
            var edad = Convert.ToString(item.edad);

            await Navigation.PushAsync(new vistaEditar(id, nombre, apellido, edad));
        }
    }
}
