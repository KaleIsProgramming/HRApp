"use client"
import React, { useState, ChangeEvent } from 'react';
import { useRouter } from 'next/navigation';

export type HolidayType =
  | "circumstantial-holiday"
  | "vacation-leave"
  | "on-demand-holiday"
  | "child-care-leave-days"
  | "child-care-leave-hours";

const HolidayTypeSelector: React.FC = () => {
  const router = useRouter();
  const [selected, setSelected] = useState<HolidayType | "">("");

  const handleChange = (e: ChangeEvent<HTMLSelectElement>) => {
    const type = e.target.value as HolidayType;
    setSelected(type);

    let route = "";
    switch (type) {
      case "circumstantial-holiday":
        route = "/circumstantial-holiday";
        break;
      case "vacation-leave":
        route = "/vacation-leave";
        break;
      case "on-demand-holiday":
        route = "/on-demand-holiday";
        break;
      case "child-care-leave-days":
        route = "/child-care-leave-days";
        break;
      case "child-care-leave-hours":
        route = "/child-care-leave-hours";
        break;
      default:
        break;
    }
    if (route) {
      router.push(route);
    }
  };

  return (
    <div className="w-full p-4 bg-gray-200">
      <select value={selected} onChange={handleChange} className="p-2 border rounded">
        <option value="">-- Wybierz rodzaj wniosku --</option>
        <option value="circumstantial-holiday">Urlop okolicznościowy</option>
        <option value="vacation-leave">Urlop wypoczynkowy</option>
        <option value="on-demand-holiday">Urlop na żądanie</option>
        <option value="child-care-leave-days">Opieka nad dzieckiem – dni</option>
        <option value="child-care-leave-hours">Opieka nad dzieckiem – godziny</option>
      </select>
    </div>
  );
};

export default HolidayTypeSelector;
