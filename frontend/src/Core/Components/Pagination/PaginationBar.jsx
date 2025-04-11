
import { RiArrowLeftSLine, RiArrowRightSLine } from "react-icons/ri";
import { useSearchParams } from "react-router-dom";
import { usePagination } from "./usePagination";

const pageKey = "page";
function PaginationBar({ totalPages , onChangePage }) {
  const [searchParams] = useSearchParams();
  const currentPage = Number(searchParams.get( pageKey)) || 1;

  const paginationRange = usePagination({
    totalPageCount: totalPages,
    currentPage,
  });

  if (!currentPage || paginationRange.length < 2) {
    return null;
  }

  let lastPage = paginationRange[paginationRange.length - 1];

  function handlePageChange(page) {
    onChangePage(page);
  }
  function prevPage() {
    if (currentPage > 1) {
      handlePageChange(currentPage - 1);
    }
  }

  function nextPage() {
    if (currentPage < totalPages) {
      handlePageChange(currentPage + 1);
    }
  }

  const baseStyle =
    "w-8 sm:w-10 md:w-12 cursor-pointer flex h-8 sm:h-10 items-center justify-center border-gray-300 bg-white leading-tight text-black hover:bg-gray-100 hover:text-gray-700 border text-gray-700 ";
  const activeStyle = " !border-blue-700 !bg-blue-100 text-blue-700 font-bold"; // Styles for the active page

  return (
    <nav aria-label="Page navigation example">
      <ul className="mx-auto flex h-8 select-none flex-wrap justify-center gap-2 -space-x-px xs:flex-nowrap sm:h-10 sm:gap-3 mt-7">
        <li>
          <button
            onClick={prevPage}
            className={`text-xl sm:text-2xl rounded-sm ${baseStyle}`}
            disabled={currentPage <= 1}
          >
            <RiArrowLeftSLine />
          </button>
        </li>
        {paginationRange?.map((value, index) => (
          <li key={index} className="block">
            {!Number.isInteger(value) ? (
              <span className="text-dark-gray pl-2 text-xl sm:pl-3 sm:text-2xl">
                ...
              </span>
            ) : (
              <button
                onClick={() => handlePageChange(value)}
                className={`${baseStyle} rounded-sm ${
                  currentPage === value ? activeStyle : ""
                }`}
              >
                {value}
              </button>
            )}
          </li>
        ))}
        <li>
          <button
            onClick={nextPage}
            className={`text-xl sm:text-2xl rounded-sm ${baseStyle}`}
            disabled={currentPage == lastPage}
          >
            <RiArrowRightSLine />
          </button>
        </li>
      </ul>
    </nav>
  );
}

export default PaginationBar;
