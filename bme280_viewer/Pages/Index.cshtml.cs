using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bme280_viewer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace bme280_viewer.Pages
{
    public class IndexModel : PageModel
    {
        private readonly bme280Context _context;
        public IList<Bme280> Bme280List { get; set; }

        public IndexModel(bme280Context context)
        {
            _context = context;
        }

        public void OnGet()
        {
            var data_list = _context.Bme280.OrderByDescending(x => x.Id).Take(1000).ToList();
            data_list.Sort((a, b) => a.Id - b.Id);
            Bme280List = data_list;
        }
    }
}
