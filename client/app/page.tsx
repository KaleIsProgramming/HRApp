import React from 'react';
import HolidayTypeSelector from '@/components/LeaveTypeSelector';

// "/" page(Home one)
const Home: React.FC = () => {
  return (
    <div className="h-screen w-[30vw] mx-auto flex items-center justify-center">
      <div className="h-[200px] w-full rounded-[10px] p-[20px] bg-[#fafafa] shadow-sm min-w-[310px] flex items-center">
        <HolidayTypeSelector />
      </div>
    </div>
  );
};

export default Home;
