"use client"
import React, { useState, FormEvent } from 'react';
import HolidayTypeSelector from '@/components/HolidayTypeSelector';

interface OnDemandHoliday {
  leaveType: string;
  startDate: string;
  endDate: string;
  sapNumber: string;
  comment: string;
}

const OnDemandLeave: React.FC = () => {
  const [startDate, setStartDate] = useState<string>("");
  const [endDate, setEndDate] = useState<string>("");
  const [sapNumber, setSapNumber] = useState<string>("");
  const [comment, setComment] = useState<string>("");
  const [message, setMessage] = useState<string>("");

  const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const requestBody: OnDemandHoliday = {
      leaveType: "Urlop na żądanie",
      startDate,
      endDate,
      sapNumber,
      comment,
    };

    try {
      const res = await fetch('https://localhost:5001/api/LeaveRequests', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
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
      <HolidayTypeSelector />
      <h1 className="text-2xl font-bold mb-4">Urlop na żądanie</h1>
      {message && <div className="mb-4 p-2 bg-blue-100">{message}</div>}
      <form onSubmit={handleSubmit} className="space-y-4">
        <div>
          <label className="block mb-1">Data początkowa:</label>
          <input
            type="date"
            value={startDate}
            onChange={(e) => setStartDate(e.target.value)}
            required
            className="w-full p-2 border rounded"
          />
        </div>
        <div>
          <label className="block mb-1">Data końcowa:</label>
          <input
            type="date"
            value={endDate}
            onChange={(e) => setEndDate(e.target.value)}
            required
            className="w-full p-2 border rounded"
          />
        </div>
        <div>
          <label className="block mb-1">Numer SAP:</label>
          <input
            type="text"
            value={sapNumber}
            onChange={(e) => setSapNumber(e.target.value)}
            required
            className="w-full p-2 border rounded"
          />
        </div>
        <div>
          <label className="block mb-1">Komentarz:</label>
          <textarea
            value={comment}
            onChange={(e) => setComment(e.target.value)}
            className="w-full p-2 border rounded"
          ></textarea>
        </div>
        <button type="submit" className="px-4 py-2 bg-blue-500 text-white rounded">
          Zapisz wniosek
        </button>
      </form>
    </div>
  );
};

export default OnDemandLeave;
