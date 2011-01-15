/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about mytrip.mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System.Web.Mvc;
using mtm.Core.Repository;

namespace mtm.Core.Controllers
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
