import { useState } from "react";
import { useSearchParams } from "react-router-dom";
import { defailtPageSize } from "../../Core/Settings/Constants";
import usePatients from "./Hooks/usePatients";
import Select from "../../Core/Components/Form/Select";
import { Table, TableBody, TableCell, TableHeader, TableRow } from "../../Core/Components/Tables/TableRelates";
import PaginationBar from "../../Core/Components/Pagination/PaginationBar";
import PatientContextMenu from "./PatientContextMenu";
import PatientRow from "./PatientRow";

export default function PatientsTable() {
  const [searchParams, setSearchParams] = useSearchParams();
  // State for filters
  const [filters, setFilters] = useState({
    searchQuery: searchParams.get("search") || "",
    gender: searchParams.get("gender") || "All",
    orderDirection: searchParams.get("orderDirection") || "asc",
    orderBy: searchParams.get("orderBy") || "id",
    pageNumber: searchParams.get("page") || 1,  
    pageSize: defailtPageSize
  });

  // Fetch patients data
  const { patients, isLoading, error } = usePatients(filters);

  function setParams(key, value){
    searchParams.set(key, value);
    setSearchParams(searchParams);
  }
  // Handle filter changes
  const handleSearchChange = (e) => {
    setFilters(prev => ({ ...prev, searchQuery: e.target.value, pageNumber: 1 }));
    setParams("search", e.target.value);
  };

  const handleGenderChange = (value) => {
    setFilters(prev => ({ ...prev, gender: value, pageNumber: 1 }));
    setParams("gender", value);
  };

  const handleOrderByChange = (value) => {
    setFilters(prev => ({ ...prev, orderBy: value }));
    setParams("orderBy", value);
  };

  const handleOrderDirectionChange = (value) => {
    setFilters(prev => ({ ...prev, orderDirection: value }));
    setParams("orderDirection", value);
  };


  return (
    <div className="overflow-hidden rounded-xl border border-gray-200 bg-white  pt-4">
      <div className="flex flex-col gap-5 px-6 mb-4 sm:flex-row sm:items-center sm:justify-between">
        <form className="">
          <div className="relative">
            <span className="absolute -translate-y-1/2 pointer-events-none top-1/2 left-4">
              <svg className="fill-gray-500 " width="20" height="20" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">
                <path fillRule="evenodd" clipRule="evenodd" d="M3.04199 9.37381C3.04199 5.87712 5.87735 3.04218 9.37533 3.04218C12.8733 3.04218 15.7087 5.87712 15.7087 9.37381C15.7087 12.8705 12.8733 15.7055 9.37533 15.7055C5.87735 15.7055 3.04199 12.8705 3.04199 9.37381ZM9.37533 1.54218C5.04926 1.54218 1.54199 5.04835 1.54199 9.37381C1.54199 13.6993 5.04926 17.2055 9.37533 17.2055C11.2676 17.2055 13.0032 16.5346 14.3572 15.4178L17.1773 18.2381C17.4702 18.531 17.945 18.5311 18.2379 18.2382C18.5308 17.9453 18.5309 17.4704 18.238 17.1775L15.4182 14.3575C16.5367 13.0035 17.2087 11.2671 17.2087 9.37381C17.2087 5.04835 13.7014 1.54218 9.37533 1.54218Z" fill=""></path>
              </svg>
            </span>
            <input 
              type="text" 
              placeholder="Search patients..." 
              className="h-11  shadow-theme-xs focus:border-brand-300 focus:ring-brand-500/10 w-full rounded-lg border border-gray-300 bg-transparent py-2.5 pr-4 pl-[42px] text-sm text-gray-800 placeholder:text-gray-400 focus:ring-3 focus:outline-hidden xl:w-[300px]  0  placeholder:text-white/30" 
              value={filters.searchQuery}
              onChange={handleSearchChange}
            />
          </div>
        </form>
        <div className="flex-grow">
          <Select
            id="gender"
            value={filters.gender}
            onChange={(value) => handleGenderChange(value)}
            options={[
              { value: "All", label: "All Genders" },
              { value: "0", label: "Male" },
              { value: "1", label: "Female" }
            ]}
          />
        </div>
        <div className="flex-grow">
          <Select
            id="orderBy"
            value={filters.orderBy}
            onChange={(value) => handleOrderByChange(value)}
            options={[
              { value: "id", label: "ID" },
              { value: "Name", label: "Name" }
            ]}
          />
        </div>
        <div className="flex-grow">
          <Select
            id="orderDirection"
            value={filters.orderDirection}
            onChange={(value) => handleOrderDirectionChange(value)}
            options={[
              { value: "asc", label: "Ascending" },
              { value: "desc", label: "Descending" }
            ]}
          />
        </div>
      </div>
      <div className="max-w-full overflow-x-auto">
        <Table>
          {/* Table Header */}
          <TableHeader className="border-b border-gray-100 ">
            <TableRow>
              <TableCell isHeader className="px-5 py-3 font-medium text-gray-500 text-start text-theme-xs ">
                Patient
              </TableCell>
              <TableCell isHeader className="px-5 py-3 font-medium text-gray-500 text-start text-theme-xs ">
                Email
              </TableCell>
              <TableCell isHeader className="px-5 py-3 font-medium text-gray-500 text-start text-theme-xs ">
                Birth Date
              </TableCell>
              <TableCell isHeader className="px-5 py-3 font-medium text-gray-500 text-start text-theme-xs ">
                Gender
              </TableCell>
              <TableCell isHeader className="px-5 py-3 font-medium text-gray-500 text-start text-theme-xs ">
                Age
              </TableCell>
            </TableRow>
          </TableHeader>

          {/* Table Body */}
          <TableBody className="divide-y divide-gray-100 ">
            {isLoading ? (
              <TableRow>
                <TableCell colSpan={5} className="px-5 py-4 text-center">
                  Loading patients...
                </TableCell>
              </TableRow>
            ) : error ? (
              <TableRow>
                <TableCell colSpan={5} className="px-5 py-4 text-center text-red-500">
                  Error loading patients: {error.message}
                </TableCell>
              </TableRow>
            ) : patients.data?.length > 0 ? (
              patients.data.map((patient) => (
                <PatientContextMenu key={patient.id} patient={patient}>
                  <PatientRow patient={patient}/>
                </PatientContextMenu>
              ))
            ) : (
              <TableRow>
                <TableCell colSpan={5} className="px-5 py-4 text-center">
                  No patients found
                </TableCell>
              </TableRow>
            )}
          </TableBody>
        </Table>
        <PaginationBar 
          totalPages={patients?.totalPages}
          onChangePage= {(page) => {
            setFilters(prev => ({ ...prev, pageNumber: page }));
            setParams("page", page);
          }}
        />
      </div>
    </div>
  );
}

