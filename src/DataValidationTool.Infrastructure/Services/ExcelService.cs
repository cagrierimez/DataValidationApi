using ClosedXML.Excel;
using DataValidationTool.Application.Dtos;
using DataValidationTool.Application.Validations;
using DocumentFormat.OpenXml.Presentation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataValidationTool.Infrastructure.Services
{
    public class ExcelService
    {
        public (List<CustomerDto> valid, List<(CustomerDto dto, List<string> errors)> invalid) ProcessExcel(string filePath)
        {
            var validList = new List<CustomerDto>();
            var invalidList = new List<(CustomerDto, List<string>)>();
            var validator = new CustomerValidator();
            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheets.First();
                var rows = worksheet.RangeUsed().RowsUsed().Skip(1); // Skip header

                foreach (var row in rows)
                {
                    var birthDateString = row.Cell(3).GetString();
                    DateTime? birthDate = null;
                    if (!string.IsNullOrWhiteSpace(birthDateString) &&
                       DateTime.TryParse(birthDateString, out DateTime parsedDate))
                    {
                        birthDate = parsedDate;
                    }
                    var dto = new CustomerDto
                    {
                        Name = row.Cell(1).GetString(),
                        Email = row.Cell(2).GetString(),
                        BirthDate = birthDate
                    };

                    ValidationResult result = validator.Validate(dto);

                    if (result.IsValid)
                        validList.Add(dto);
                    else
                        invalidList.Add((dto, result.Errors.Select(e => e.ErrorMessage).ToList()));
                }
            }

            return (validList, invalidList);
        }
        public string ExportInvalidRecords(List<(CustomerDto dto, List<string> errors)> invalidRecords)
        {
            var filePath = Path.Combine(Path.GetTempPath(), $"InvalidRecords_{DateTime.Now:yyyyMMddHHmmss}.xlsx");

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.AddWorksheet("Invalid Records");
                worksheet.Cell(1, 1).Value = "Name";
                worksheet.Cell(1, 2).Value = "Email";
                worksheet.Cell(1, 3).Value = "BirthDate";
                worksheet.Cell(1, 4).Value = "Errors";

                int row = 2;
                foreach (var item in invalidRecords)
                {
                    worksheet.Cell(row, 1).Value = item.dto.Name;
                    worksheet.Cell(row, 2).Value = item.dto.Email;
                    worksheet.Cell(row, 3).Value = item.dto.BirthDate?.ToString("yyyy-MM-dd");
                    worksheet.Cell(row, 4).Value = string.Join("; ", item.errors);
                    row++;
                }

                workbook.SaveAs(filePath);
            }

            return filePath;
        }
    }
}
