using DocumentFormat.OpenXml.Packaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDO_QuestionParser.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        char[] splitchars = { '\n' };

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public RedirectToPageResult OnPostByWordFile(IFormFile file, bool isAutomatic)
        {
            List<string> data = new List<string>();

            using (WordprocessingDocument word = WordprocessingDocument.Open(file.OpenReadStream(), false))
            {
                data.AddRange(word.MainDocumentPart.Document.Body.InnerText.Split(splitchars));
            }
            TempData.SetJson("qa_data", data);
            return isAutomatic ?
                RedirectToPage("./AutomaticEdit") :
                RedirectToPage("./HandEdit");
        }

        public RedirectToPageResult OnPostByString(string input, bool isAutomatic)
        {
            List<string> data = new List<string>();

            data.AddRange(input.Split(splitchars));

            TempData.SetJson("qa_data", data);
            return isAutomatic ?
                RedirectToPage("./AutomaticEdit") :
                RedirectToPage("./HandEdit");
        }

    }
}
