using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebStore.Domain;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.ServiceHosting.Controllers
{
    /// <summary>
    /// Контроллер  управления сотрудниками
    /// </summary>
    [Route(WebAPI.Employees)]
    [ApiController]
    public class EmployeesApiController : ControllerBase, IEmployeesData
    {
        private readonly IEmployeesData _EmployeesData;

        public EmployeesApiController(IEmployeesData EmployeesData) => _EmployeesData = EmployeesData;

        /// <summary>
        /// Получить всех сотрудников
        /// </summary>
        /// <returns>Список сотрудников магазина</returns>
        [HttpGet]
        public IEnumerable<Employee> GetAll() => _EmployeesData.GetAll();

        /// <summary>
        /// Получить сотрудника по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сотрудника</param>
        /// <returns>Сотрудник с указанным идентификатором</returns>
        [HttpGet("{id}")]
        public Employee GetById(int id) => _EmployeesData.GetById(id);

        /// <summary>
        /// Добавить сотрудника
        /// </summary>
        /// <param name="Employee">Новый сотрудник магазина</param>
        [HttpPost]
        public void Add([FromBody] Employee Employee) => _EmployeesData.Add(Employee);

        /// <summary>
        /// Редактирование сотрудника
        /// </summary>
        /// <param name="id">Идентификатор редактируемого сотрудника</param>
        /// <param name="Employee">Информация, вносимая в БД о сотруднике</param>
        [HttpPut("{id}")]
        public void Edit(int id, [FromBody] Employee Employee) => _EmployeesData.Edit(id, Employee);

        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        /// <param name="id">Идентификатор сотрудника</param>
        /// <returns>Подтверждение, если сотрудник удалён</returns>
        [HttpDelete("{id}")]
        public bool Delete(int id) => _EmployeesData.Delete(id);

        [NonAction]
        public void SaveChanges() => _EmployeesData.SaveChanges();
    }
}