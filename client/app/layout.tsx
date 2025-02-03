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
      <body>{children}</body>
    </html>
  );
}
