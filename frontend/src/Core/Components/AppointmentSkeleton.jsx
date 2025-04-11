import React from 'react';
//viewType can be 'day', 'week', or 'month'
export const AppointmentSkeleton = ({ viewType }) => {
  const getSkeletonItems = () => {
    switch (viewType) {
      case 'day':
        return (
          <div className="space-y-2">
            {[...Array(8)].map((_, i) => (
              <div key={i} className="h-16 bg-gray-200 rounded-md animate-pulse"></div>
            ))}
          </div>
        );
      case 'week':
        return (
          <div className="grid grid-cols-7 gap-1">
            {[...Array(7)].map((_, dayIndex) => (
              <div key={dayIndex} className="space-y-1">
                {[...Array(5)].map((_, i) => (
                  <div key={i} className="h-20 bg-gray-200 rounded animate-pulse"></div>
                ))}
              </div>
            ))}
          </div>
        );
      case 'month':
        return (
          <div className="grid grid-cols-7 gap-1">
            {[...Array(42)].map((_, i) => (
              <div key={i} className="h-24 bg-gray-200 rounded animate-pulse"></div>
            ))}
          </div>
        );
      default:
        return null;
    }
  };

  return (
    <div className="p-4">
      {getSkeletonItems()}
    </div>
  );
};