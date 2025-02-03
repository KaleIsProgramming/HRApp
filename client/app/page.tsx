import React from 'react';
import HolidayTypeSelector from '@/components/HolidayTypeSelector';

const Home: React.FC = () => {
  return (
    <div className="min-h-screen flex flex-col items-center justify-center p-4">
      <HolidayTypeSelector />
      <h1 className="text-3xl font-bold mt-4">
        Wybierz rodzaj wniosku urlopowego
      </h1>
    </div>
  );
};

export default Home;
