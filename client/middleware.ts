import { NextResponse } from "next/server";
import type { NextRequest } from "next/server";

// Allowed urls
const allowedPaths = [
  "/",
  "/child-care-leave-days",
  "/child-care-leave-hours",
  "/circumstantial-leave",
  "/on-demand-leave",
  "/vacation-leave"
];

export function middleware(request: NextRequest) {
  const { pathname } = request.nextUrl;

  // Automaticly generated Next.js and favicon url's
  if (pathname.startsWith("/_next") || pathname.startsWith("/api") || pathname === "/favicon.ico") {
    return NextResponse.next();
  }

  if (!allowedPaths.includes(pathname)) {
    return NextResponse.redirect(new URL("/", request.url));
  }

  return NextResponse.next();
}

export const config = {
  matcher: ["/((?!_next/static|_next/image|favicon.ico).*)"],
};
