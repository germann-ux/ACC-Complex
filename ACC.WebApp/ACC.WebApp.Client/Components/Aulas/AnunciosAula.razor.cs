using Microsoft.AspNetCore.Components;

namespace ACC.WebApp.Client.Components.Aulas;

public partial class AnunciosAula : ComponentBase
{
    public class Anuncio
    {
        public string Titulo { get; set; } = string.Empty;
        public string Cuerpo { get; set; } = string.Empty;
        public DateTime Fecha { get; set; } = DateTime.Now;
    }

    private List<Anuncio> anuncios = new();
    private Anuncio current = new();
    private bool showForm;
    private bool editing;
    private Anuncio? editingTarget;

    private void Nuevo()
    {
        current = new Anuncio();
        editingTarget = null;
        editing = false;
        showForm = true;
    }

    private void Editar(Anuncio anuncio)
    {
        editingTarget = anuncio;
        current = new Anuncio { Titulo = anuncio.Titulo, Cuerpo = anuncio.Cuerpo, Fecha = anuncio.Fecha };
        editing = true;
        showForm = true;
    }

    private void Guardar()
    {
        if (editing && editingTarget != null)
        {
            editingTarget.Titulo = current.Titulo;
            editingTarget.Cuerpo = current.Cuerpo;
        }
        else
        {
            current.Fecha = DateTime.Now;
            anuncios.Add(current);
        }
        showForm = false;
    }

    private void Cancelar()
    {
        showForm = false;
    }

    private void Eliminar(Anuncio anuncio)
    {
        anuncios.Remove(anuncio);
    }
}
