/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about Mytrip.Mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System.Web.Mvc;
using Mytrip.Mvc.Repository;

namespace Mytrip.Mvc.Controllers
{
    /// <summary>Контроллер инициализации Repository
    /// </summary>
    public class ICoreController : Controller
    {
        ICoreRepository _coreRepo;

        /// <summary> Инициализация Repository
        /// </summary>
        internal ICoreRepository coreRepo
        {
            get
            {
                if (_coreRepo == null)
                    _coreRepo = new ICoreRepository();
                return _coreRepo;
            }
        }

    }
}
