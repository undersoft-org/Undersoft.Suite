/*
 * bitscan.h
 *
 *  Created on: 31 mar 2021
 *  Author: Darius Hanc
 */
#ifndef _SUBMIX_H_
#define _SUBMIX_H_

#ifdef SUBMIX_EXPORTS
#define SUBMIX_API __declspec(dllexport)
#else
#define SUBMIX_API __declspec(dllimport)
#endif

#pragma unmanaged
#include "bitscan.h"

extern "C"
{
	SUBMIX_API uint32_t SubmixMap32(const uint32_t src, const uint32_t dest);

	SUBMIX_API uint32_t SubmixMapToMask32(const uint32_t src, const uint32_t dest, const uint32_t bitmask, const int32_t msbid);

	SUBMIX_API uint32_t SubmixMask32(const uint32_t dest);

	SUBMIX_API int32_t	SubmixMsbId32(const uint32_t dest);

	SUBMIX_API uint64_t SubmixMap64(const uint64_t src, const uint64_t dest);

	SUBMIX_API uint64_t SubmixMapToMask64(const uint64_t src, const uint64_t dest, const uint64_t bitmask, const int32_t msbid);

	SUBMIX_API uint64_t SubmixMask64(const uint64_t dest);

	SUBMIX_API int32_t SubmixMsbId64(const uint64_t dest);
}


#endif /* _SUBMIX_H_ */
