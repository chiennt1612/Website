using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace WebClient.Services.Interfaces
{
    public interface IViewDataParam
    {
        ViewDataDictionary viewData { get; set; }
        void setViewData();
    }
}
