using EmployeeModels.ViewModel;
using EmployeeServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public EmployeeController(IEmployeeService employeeService, IWebHostEnvironment webHostEnvironment)
        {
            _employeeService = employeeService;
            _webHostEnvironment = webHostEnvironment;

        }

        /// <summary>Gets the employee by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// 
        [HttpGet]
        public ActionResult GetEmployeeById(int? id)
        {
            try
            {
                var employee = _employeeService.GetEmployeeById(id);
                return View(employee);
            }
            catch
            {
                ViewBag.ErrorMessage = "Employee not found.";
            }
            return View();
        }
        
        [HttpGet]
        public ActionResult AddEmployee()
        {
            var designations = _employeeService.GetDesignations();
            ViewBag.Designations = new SelectList(designations, "DesignationId", "Designation");
            return View("AddEmployee");
        }
        /// <summary>Adds the employee.</summary>
        /// <param name="empDetail">The emp detail.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        ////  [HttpPost]
        public ActionResult AddEmployee(EmployeeDetailView empDetail) 
        {
            try
            {
                if (Request.Form.Files.Count > 0)
                {
                    //save image in server
                    IFormFile file = Request.Form.Files[0];
                    string fileName = Path.GetFileName(file.FileName);
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                    string filePath = Path.Combine(uploadsFolder, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    empDetail.ProfilePicture = fileName;
                }
                //pass details to repository
                _employeeService.AddEmployee(empDetail);
                //TempData["Success"] = "Employee Created Successfully";
                return RedirectToAction("GetAll");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Failed to add employee. Error: " + ex.Message;
                return View();
            }
        }
        /// <summary>Edits the employee.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpGet]                                                   
        public ActionResult EditEmployee(int? id)
        {
            try
            {
                var employees = _employeeService.GetEmployeeById(id);
                var designations = _employeeService.GetDesignations();
                ViewBag.Designations = new SelectList(designations, "DesignationId", "Designation");
                if (employees == null)
                {
                    ViewBag.ErrorMessage = "Employee is null";
                    return View();
                }
                return View(employees);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Failed to retrieve employee details. Error: " + ex.Message;
                return View();
            }
        }
        /// <summary>Edits the employee.</summary>
        /// <param name="detail">The detail.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpPost]
        public ActionResult EditEmployee(EmployeeDetailView detail)
        {
            try
            {
                if (Request.Form.Files.Count > 0)
                {
                    // Save image on the server
                    IFormFile file = Request.Form.Files[0];
                    string fileName = file.FileName;
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                    string filePath = Path.Combine(uploadsFolder, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    detail.ProfilePicture = fileName;
                }
                // Update employee details using the service
                _employeeService.UpdateEmployee(detail);
                TempData["Success"] = "Employee Updated Successfully";
                return RedirectToAction("GetAll");
            }
            catch (Exception ex)
            {
                throw ex;
                //ViewBag.ErrorMessage = "Failed to update employee. Error: " + ex.Message;
                //// If an error occurs, return to the edit view with the current model
                //return View(detail);
            }
        }
        /// <summary>Deletes the employee.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpPost]
        public ActionResult DeleteEmployee(int? id)
        {
            
                // Delete logic here
                try
                {
                    _employeeService.DeleteEmployee(id);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                TempData["Success"] = "Employee Deleted Successfully";
            
            return RedirectToAction("GetAll");
        }

        /// <summary>Gets all.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                List<EmployeeDetailView> employees = _employeeService.GetAllEmployee();
                return View(employees);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}