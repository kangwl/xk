using System.Web;

namespace XK.DataApi.Source.Act {
    public interface IAct {
        string Add(HttpRequest request);
        string List(HttpRequest request);
        string Delete(HttpRequest request);
        string Update(HttpRequest request);
        string GetOne(HttpRequest request);
    }
}