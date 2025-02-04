import './globals.css';
import { ReactNode } from 'react';

export const metadata = {
  title: 'HR App',
  description: 'Application for holiday requests',
};

export default function RootLayout({
  children,
}: {
  children: ReactNode;
}) {
  return (
    <html lang="en">
      <body className='bg-[#f0ebf8]'>{children}</body>
    </html>
  );
}
