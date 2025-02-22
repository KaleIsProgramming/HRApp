"use client"
import React, { useState, FormEvent } from 'react';
import LeaveTypeSelector from '@/components/LeaveTypeSelector';

interface ChildCareLeaveDaysInterface {
  leaveType: string;
  startDate: string;
  endDate: string;
  comment: string;
}

interface ErrorsInterface {
  startDate: string;
  endDate: string;
  comment: string;
}

const ChildCareLeaveDays: React.FC = () => {
  const [startDate, setStartDate] = useState<string>("");
  const [endDate, setEndDate] = useState<string>("");
  const [comment, setComment] = useState<string>("");
  const [message, setMessage] = useState<string>("");

  const [errors, setErrors] = useState<ErrorsInterface>({
    startDate: "",
    endDate: "",
    comment: ""
  });

  //Front End validation handling
  const validate = () => {
    let valid = true;
    const newErrors = { startDate: "", endDate: "", comment: "" };
    if (!startDate) {
      newErrors.startDate = "To pole jest wymagane.";
      valid = false;
    }
    if (!endDate) {
      newErrors.endDate = "To pole jest wymagane.";
      valid = false;
    }
    if (startDate && endDate && new Date(startDate) > new Date(endDate)) {
      newErrors.endDate = "Data końcowa musi być późniejsza niż data początkowa.";
      valid = false;
    }
    if (comment && comment.length > 500) {
      newErrors.comment = "Komentarz nie może przekraczać 500 znaków.";
      valid = false;
    }
    setErrors(newErrors);
    return valid;
  };

  //Front-End connection with Back-End
  const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    if (!validate()) return;
    const requestBody: ChildCareLeaveDaysInterface = {
      leaveType: "ChildCareLeaveDays",
      startDate,
      endDate,
      comment,
    };


    try {
      const res = await fetch('https://localhost:5001/api/LeaveRequests', {
        method: 'POST',
        headers: { 
          'Content-Type': 'application/json',
          'Authorization': 'Bearer hello_backend' 
        },
        body: JSON.stringify(requestBody),
      });
      const data = await res.json();
      setMessage(res.ok ? data.message : data.message || "Błąd podczas zapisu.");
    } catch (error) {
      setMessage("Błąd podczas łączenia z backendem.");
    }
  };

  return (
    <div className="max-w-xl mx-auto p-4">
      <LeaveTypeSelector />
      <h1 className="text-2xl font-bold mb-4">Opieka nad dzieckiem – dni</h1>
      {message && <div className="mb-4 p-2 bg-blue-100">{message}</div>}
      <form onSubmit={handleSubmit} className="space-y-4">
        <div>
          <label className="block mb-1">Data początkowa:</label>
          <input
            type="date"
            value={startDate}
            onChange={(e) => setStartDate(e.target.value)}
            className="w-full p-2 border rounded"
          />
          {errors.startDate && <p className="text-red-500 text-sm">{errors.startDate}</p>}
        </div>
        <div>
          <label className="block mb-1">Data końcowa:</label>
          <input
            type="date"
            value={endDate}
            onChange={(e) => setEndDate(e.target.value)}
            className="w-full p-2 border rounded"
          />
          {errors.endDate && <p className="text-red-500 text-sm">{errors.endDate}</p>}
        </div>
        <div>
          <label className="block mb-1">Komentarz:</label>
          <textarea
            value={comment}
            onChange={(e) => setComment(e.target.value)}
            className="w-full p-2 border rounded"
          ></textarea>
          {errors.comment && <p className="text-red-500 text-sm">{errors.comment}</p>}
        </div>
        <button type="submit" className="px-4 py-2 bg-blue-500 text-white rounded">
          Zapisz wniosek
        </button>
      </form>
    </div>
  );
};

export default ChildCareLeaveDays;