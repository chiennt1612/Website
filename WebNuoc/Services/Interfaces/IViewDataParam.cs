using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace WebNuoc.Services.Interfaces
{
    public interface IViewDataParam
    {
        ViewDataDictionary viewData { get; set; }
        void setViewData();
    }
}
