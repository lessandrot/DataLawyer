using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DataLawyer.Api.Controllers
{
    [ApiController]
    public abstract class  BaseController : ControllerBase
    {
        private ActionResult _result;        

        protected async Task<ActionResult> Execute(Action action)
        {
            return await Task.Run(() =>
            {
                try
                {
                    action.Invoke();
                    Success();
                }
                catch (Exception ex) { Error(ex); }

                return _result;
            });
        }

        protected async Task<ActionResult> Execute<TResult>(Func<TResult> action)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var result = action.Invoke();
                    Success(result);
                }
                catch (Exception ex) { Error(ex); }

                return _result;
            });
        }

        protected void Success(object value = null) => _result = Ok(value);

        protected void Error(Exception ex)
        {
            var model = new ModelStateDictionary();
            model.AddModelError("Error", ex.Message);
            _result = BadRequest(model);
        }
    }
}