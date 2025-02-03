"use client"
import React, { useState, FormEvent } from 'react';
import { useRouter } from 'next/navigation';
import HolidayTypeSelector from '@/components/HolidayTypeSelector';

interface CircumstantialHolidayInterface {
  leaveType: string;
  startDate: string;
  endDate: string;
  okolicznościowyType: string;
  fullName: string;
  comment: string;
}

const CircumstantialHoliday: React.FC = () => {
  const router = useRouter();
  const [startDate, setStartDate] = useState("");
  const [endDate, setEndDate] = useState("");
  const [okolicznościowyType, setOkolicznościowyType] = useState("");
  const [fullName, setFullName] = useState("");
  const [comment, setComment] = useState("");
  const [message, setMessage] = useState("");

  const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const requestBody: CircumstantialHolidayInterface = {
      leaveType: "Urlop okolicznościowy",
      startDate,
      endDate,
      okolicznościowyType,
      fullName,
      comment,
    };

    try {
      const res = await fetch('https://localhost:5001/api/HolidayRequests', {
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
      <h1 className="text-2xl font-bold mb-4">Urlop okolicznościowy</h1>
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
          <label className="block mb-1">Rodzaj urlopu okolicznościowego:</label>
          <select
            value={okolicznościowyType}
            onChange={(e) => setOkolicznościowyType(e.target.value)}
            required
            className="w-full p-2 border rounded"
          >
            <option value="">-- Wybierz --</option>
            <option value="Ślub pracownika">Ślub pracownika</option>
            <option value="Narodziny dziecka">Narodziny dziecka</option>
            <option value="Ślub dziecka">Ślub dziecka</option>
          </select>
        </div>
        <div>
          <label className="block mb-1">Imię i Nazwisko osoby:</label>
          <input
            type="text"
            value={fullName}
            onChange={(e) => setFullName(e.target.value)}
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

export default CircumstantialHoliday;
