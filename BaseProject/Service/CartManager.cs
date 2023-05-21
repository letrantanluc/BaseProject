using BaseProject.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseProject.Service
{
    public class CartManager
    {
        public const string CartSessionKey = "Cart";
        public List<CartItem> GetCartItems()
        {
            var cart = HttpContext.Current.Session[CartSessionKey] as List<CartItem>;
            if (cart == null)
            {
                cart = new List<CartItem>();
                HttpContext.Current.Session[CartSessionKey] = cart;
            }
            return cart;
        }
        public void AddToCart(CartItem item)
        {
            var cart = GetCartItems();
            var existingItem = cart.FirstOrDefault(c => c.ProductId == item.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                cart.Add(item);
            }
        }
        public void UpdateCart(IEnumerable<CartItem> cart)
        {
            HttpContext.Current.Session[CartSessionKey] = cart.ToList();
        }

        public void RemoveFromCart(Guid cartItemId)
        {
            var cart = GetCartItems();
            var existingItem = cart.FirstOrDefault(c => c.Id == cartItemId);
            if (existingItem != null)
            {
                cart.Remove(existingItem);
            }
        }
        public decimal GetCartTotal(IEnumerable<CartItem> cart)
        {
            decimal total = 0;
            foreach (var item in cart)
            {
                total += item.Price * item.Quantity;
            }
            return total;
        }
        public decimal GetTotal()
        {
            var cart = GetCartItems();
            decimal total = 0;

            foreach (var item in cart)
            {
                total += item.Price * item.Quantity;
            }

            return total;
        }

        public void ClearCart()
        {
            var cart = GetCartItems();
            cart.Clear();
        }

        
    }
}
