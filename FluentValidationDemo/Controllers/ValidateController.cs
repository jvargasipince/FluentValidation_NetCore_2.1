using FluentValidation;
using FluentValidationDemo.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FluentValidationDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidateController : ControllerBase
    {
        private readonly IValidator<EntityToValidate> _entityToValidateValidator;

        public ValidateController(IValidator<EntityToValidate> entityToValidateValidator)
        {
            _entityToValidateValidator = entityToValidateValidator;
        }

        [HttpPost("PostWithOutFluentValidate")]
        public List<string> PostWithOutFluentValidate([FromBody] EntityToValidate entity)
        {
            List<string> response = new List<string>();

            List<string> errors = new List<string>();

            if (!string.IsNullOrWhiteSpace(entity.FullName))
            {
                if (entity.FullName.Length > 50)
                    errors.Add("The length max to FullName is 50");
            }
            else
            {
                errors.Add("Please specify a fullname");
            }

            if (entity.Age < 18)
            {
                errors.Add("The min age is 18");
            }

            if (errors.Count > 0)
            {
                response.AddRange(errors);
            }
            else
            {
                response.Add("All is OK");
            }

            return response;
        }


        [HttpPost("PostWithFluentValidate")]
        public List<string> PostWithFluentValidate([FromBody] EntityToValidate entity)
        {
            List<string> response = new List<string>();

            var validationResult = _entityToValidateValidator.Validate(entity);

            if (!validationResult.IsValid)
            {

                foreach (var error in validationResult.Errors)
                {
                    response.Add(error.ToString());
                }

            }
            else
            {
                response.Add("All is OK");
            }

            return response;
        }
    }
}