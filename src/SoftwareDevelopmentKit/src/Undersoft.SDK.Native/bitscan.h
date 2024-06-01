/*
 * bitscan.h
 *
 *  Created on: 31 mar 2021
 *  Author: Darius Hanc
 */
#ifndef _BITSCAN_H_
#define _BITSCAN_H_

#ifdef BITSCAN_EXPORTS
#define BITSCAN_API __declspec(dllexport)
#else
#define BITSCAN_API __declspec(dllimport)
#endif

#pragma unmanaged
#include "scan.h"

extern "C"
{
	BITSCAN_API unsigned int ReverseIndex32(const unsigned int x);

	BITSCAN_API unsigned int ForwardIndex32(const unsigned int x);

	BITSCAN_API unsigned int LengthBefore32(const unsigned int x);

	BITSCAN_API unsigned int LengthAfter32(const unsigned int x);

	BITSCAN_API unsigned int ReverseIndex64(const unsigned long x);

	BITSCAN_API unsigned int ForwardIndex64(const unsigned long x);

	BITSCAN_API unsigned int LengthBefore64(const unsigned long x);

	BITSCAN_API unsigned int LengthAfter64(const unsigned long x);
}


#endif /* _BITSCAN_H_ */