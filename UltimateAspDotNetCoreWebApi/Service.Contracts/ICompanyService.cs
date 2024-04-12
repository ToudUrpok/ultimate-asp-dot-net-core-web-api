﻿using Shared.DataTransferObjects.Company;

namespace Service.Contracts;

public interface ICompanyService
{
    IEnumerable<CompanyDto> GetAll(bool trackChanges);
}
