using Microsoft.AspNetCore.Razor.TagHelpers;
using ShopApp.WebUI.Models;
using System.Text;

namespace ShopApp.WebUI.TegHelpers
{ 
    
    [HtmlTargetElement("span",Attributes="page-model")]
    public class TakeLinkTagHelper:TagHelper
    {
        public PageInfo PageModel { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 1; i < PageModel.TotalPages(); i++)
            {
                
                if (string.IsNullOrEmpty(PageModel.CurrenCategory))
                {
                    stringBuilder.AppendFormat("<a ' href='/AllList?page={0}'>{0}</a>", i);
                }
                else
                {
                    stringBuilder.AppendFormat("<a' href='/AllList/{1}?page={0}'>{0}</a>", i, PageModel.CurrenCategory);
                }

            }

            output.Content.SetHtmlContent(stringBuilder.ToString());
            base.Process(context, output);
        }
    }
}
