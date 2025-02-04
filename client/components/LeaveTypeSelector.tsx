"use client"
import React, { useState, ChangeEvent } from 'react';
import { useRouter } from 'next/navigation';

export type LeaveType =
  | "circumstantial-leave"
  | "vacation-leave"
  | "on-demand-leave"
  | "child-care-leave-days"
  | "child-care-leave-hours";

const LeaveTypeSelector: React.FC = () => {
  const router = useRouter();
  const [selected, setSelected] = useState<LeaveType | "">("");

  const handleChange = (e: ChangeEvent<HTMLSelectElement>) => {
    const type = e.target.value as LeaveType;
    setSelected(type);

    let route = "";
    switch (type) {
      case "circumstantial-leave":
        route = "/circumstantial-leave";
        break;
      case "vacation-leave":
        route = "/vacation-leave";
        break;
      case "on-demand-leave":
        route = "/on-demand-leave";
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
    <div className="w-full mb-4">
      <h2 className='text-2xl font-bold'>Wybierz rodzaj wniosku urlopowego</h2>
      <select value={selected} onChange={handleChange} className="p-2  mt-4 border rounded">
        <option value="">-- Wybierz rodzaj wniosku --</option>
        <option value="circumstantial-leave">Urlop okolicznościowy</option>
        <option value="vacation-leave">Urlop wypoczynkowy</option>
        <option value="on-demand-leave">Urlop na żądanie</option>
        <option value="child-care-leave-days">Opieka nad dzieckiem – dni</option>
        <option value="child-care-leave-hours">Opieka nad dzieckiem – godziny</option>
      </select>
    </div>
  );
};

export default LeaveTypeSelector;
