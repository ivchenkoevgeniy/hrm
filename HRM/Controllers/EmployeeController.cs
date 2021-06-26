using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using HRM.Domain;
using HRM.Domain.DTO;
using HRM.Dto;
using Newtonsoft.Json;

namespace HRM.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public ActionResult Index()
        {
            ViewBag.Employees = _employeeService.GetAll();
            return View();
        }

        [HttpPost]
        public JsonResult Create(EmployeeDto r)
        {
            var res = new ResponseDto();
            try
            {
                _employeeService.Create(r);
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                res.ErrorMsg = e.GetBaseException().Message;
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Import(List<EmployeeDto> importItems)
        {
            var res = new ResponseDto();
            try
            {
                var importResult = _employeeService.Import(importItems);
                res.Data = new
                {
                    errors = importResult.Errors.Distinct(),
                    modified = importResult.Modified.Distinct(),
                    created = importResult.Created.Distinct()
                };
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                res.ErrorMsg = e.GetBaseException().Message;
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Update(Guid id, EmployeeDto r)
        {
            var res = new ResponseDto();
            try
            {
                _employeeService.Update(r);
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                res.ErrorMsg = e.GetBaseException().Message;
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(Guid id)
        {
            var res = new ResponseDto();
            try
            {
                _employeeService.Remove(id);
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                res.ErrorMsg = e.GetBaseException().Message;
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public string GetData()
        {
            return JsonConvert.SerializeObject(_employeeService.GetAll());
        }
    }
}