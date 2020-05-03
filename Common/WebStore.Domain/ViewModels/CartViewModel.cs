using System.Collections.Generic;
using System.Linq;
using WebStore.ViewModels;

namespace WebStore.Domain.ViewModels
{
    public class CartViewModel
    {
        public Dictionary<ProductViewModel, int> Items { get; set; } = new Dictionary<ProductViewModel, int>();

        public int ItemsCount => Items?.Sum(item => item.Value) ?? 0;
    }
}
