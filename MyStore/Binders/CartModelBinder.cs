using MyStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace MyStore.Binders
{
    public class CartModelBinder : IModelBinder
    {
        private const string sessionKey  = "Cart";
        public bool BindModel(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
        {
            Cart cart = (Cart)modelBindingExecutionContext.HttpContext.Session[sessionKey];
            if(cart == null)
            {
                cart = new Cart();
                modelBindingExecutionContext.HttpContext.Session[sessionKey] = cart;
            }
            return cart;
        }
    }
}