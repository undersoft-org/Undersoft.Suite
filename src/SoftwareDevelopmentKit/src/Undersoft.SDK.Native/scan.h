/*
 * scan.h
 *
 *  Created on: 31 mar 2021
 * 
 */

#ifndef _SCAN_H_
#define _SCAN_H_

#include "base.h"
#include "config.h"

#if defined(_M_AMD64) || defined(_M_X64)
#include <intrin.h>
#elif defined(SSE2OPT) || defined(SSE42OPT)
#include <emmintrin.h>
#elif defined(AVX2OPT)
#include <emmintrin.h>
#include <avx2intrin.h>
#endif

#ifdef __GNUG__
#pragma GCC diagnostic push
#pragma GCC diagnostic ignored "-Wconversion"
#endif

#ifdef _MSC_VER
#pragma warning( push )
#pragma warning( disable : 4146)
#endif

 /**
  \brief ad-hoc conditional expressions
  \internal
  */
template<bool b> struct conditional
{
	static bool test()
	{
		return true;
	}
};
template<> struct conditional<false>
{
	static bool test()
	{
		return false;
	}
};


/**
	Portable LZCNT with (uses minimal LUT)
	@ingroup bitfunc
	@internal
*/
FORCEINLINE
unsigned count_leading_zeros(unsigned x) NOEXCEPT
{
	unsigned n =
		(x >= (1U << 16)) ?
		((x >= (1U << 24)) ? ((x >= (1 << 28)) ? 28u : 24u) : ((x >= (1U << 20)) ? 20u : 16u))
		:
		((x >= (1U << 8)) ? ((x >= (1U << 12)) ? 12u : 8u) : ((x >= (1U << 4)) ? 4u : 0u));
	return unsigned(lzcnt_table<true>::_lut[x >> n]) - n;
}

/**
	Portable TZCNT with (uses 37-LUT)
	@ingroup bitfunc
	@internal
*/
FORCEINLINE
unsigned count_trailing_zeros(unsigned v) NOEXCEPT
{
	// (v & -v) isolates the last set bit
	return unsigned(tzcnt_table<true>::_lut[(-v & v) % 37]);
}

template<typename T>
FORCEINLINE T ilog2_LUT(T x) NOEXCEPT
{
	unsigned l = 0;
	if (x & 0xffff0000)
	{
		l += 16; x >>= 16;
	}

	if (x & 0xff00)
	{
		l += 8; x >>= 8;
	}
	return l + T(first_bit_table<true>::_idx[x]);
}

template<>
FORCEINLINE gap_word_t ilog2_LUT<gap_word_t>(gap_word_t x) NOEXCEPT
{
	if (x & 0xff00)
	{
		x = gap_word_t(x >> 8u);
		return gap_word_t(8u + first_bit_table<true>::_idx[x]);
	}
	return gap_word_t(first_bit_table<true>::_idx[x]);
}


// if we are running on x86 CPU we can use inline ASM

#ifdef _x86
#ifdef __GNUG__

FORCEINLINE
unsigned bsf_asm32(unsigned int v) NOEXCEPT
{
	unsigned r;
	asm volatile(" bsfl %1, %0": "=r"(r) : "rm"(v));
	return r;
}

FORCEINLINE
unsigned bsr_asm32(unsigned int v) NOEXCEPT
{
	unsigned r;
	asm volatile(" bsrl %1, %0": "=r"(r) : "rm"(v));
	return r;
}

#endif  // __GNUG__

#ifdef _MSC_VER

#if defined(_M_AMD64) || defined(_M_X64) // inline assembly not supported

FORCEINLINE
unsigned int bsr_asm32(unsigned int value) NOEXCEPT
{
	unsigned long r;
	_BitScanReverse(&r, value);
	return r;
}

FORCEINLINE
unsigned int bsf_asm32(unsigned int value) NOEXCEPT
{
	unsigned long r;
	_BitScanForward(&r, value);
	return r;
}

#else

FORCEINLINE
unsigned int bsr_asm32(unsigned int value) NOEXCEPT
{
	__asm  bsr  eax, value
}

FORCEINLINE
unsigned int bsf_asm32(unsigned int value) NOEXCEPT
{
	__asm  bsf  eax, value
}

#endif

#endif // _MSC_VER

#endif // _x86


// From:
// http://citeseerx.ist.psu.edu/viewdoc/summary?doi=10.1.1.37.8562
//
template<typename T>
FORCEINLINE T bit_scan_fwd(T v) NOEXCEPT
{
	return
		DeBruijn_bit_position<true>::_multiply[(((v & -v) * 0x077CB531U)) >> 27];
}

FORCEINLINE
unsigned bit_scan_reverse32(unsigned w) NOEXCEPT
{
#if defined(_USE_GCC_BUILD) || (defined(__GNUG__) && (defined(__arm__) || defined(__aarch64__)))
	return (unsigned)(31 - __builtin_clz(w));
#else
# if defined(_x86) && (defined(__GNUG__) || defined(_MSC_VER))
	return bsr_asm32(w);
# else
	return ilog2_LUT<unsigned int>(w);
# endif
#endif
}

FORCEINLINE
unsigned bit_scan_forward32(unsigned w) NOEXCEPT
{
#if defined(_USE_GCC_BUILD) || (defined(__GNUG__) && (defined(__arm__) || defined(__aarch64__)))
	return (unsigned)__builtin_ctz(w);
#else
# if defined(_x86) && (defined(__GNUG__) || defined(_MSC_VER))
	return bsf_asm32(w);
# else
	return bit_scan_fwd(w);
# endif
#endif
}


FORCEINLINE
unsigned long long bmi_bslr_u64(unsigned long long w) NOEXCEPT
{
#if defined(AVX2OPT) || defined (AVX512OPT)
	return _blsr_u64(w);
#else
	return w & (w - 1);
#endif
}

FORCEINLINE
unsigned long long bmi_blsi_u64(unsigned long long w)
{
#if defined(AVX2OPT) || defined (AVX512OPT)
	return _blsi_u64(w);
#else
	return w & (-w);
#endif
}

/// 32-bit bit-scan reverse
inline
unsigned count_leading_zeros_u32(unsigned w) NOEXCEPT
{
#if defined(AVX2OPT) || defined (AVX512OPT)
	return (unsigned)_lzcnt_u32(w);
#else
#if defined(USE_GCC_BUILD) || defined(__GNUG__)
	return (unsigned)__builtin_clz(w);
#else
	return count_leading_zeros(w); // portable
#endif
#endif
}


/// 64-bit bit-scan reverse
inline
unsigned count_leading_zeros_u64(id64_t w) NOEXCEPT
{
#if defined(AVX2OPT) || defined (AVX512OPT)
	return (unsigned)_lzcnt_u64(w);
#else
#if defined(USE_GCC_BUILD) || (defined(__GNUG__) && (defined(__arm__) || defined(__aarch64__)))
	return (unsigned)__builtin_clzll(w);
#else
	unsigned z;
	unsigned w1 = unsigned(w >> 32);
	if (!w1)
	{
		z = 32;
		w1 = unsigned(w);
		z += 31 - bit_scan_reverse32(w1);
	}
	else
	{
		z = 31 - bit_scan_reverse32(w1);
	}
	return z;
#endif
#endif
}

/// 32-bit bit-scan fwd
inline
unsigned count_trailing_zeros_u32(unsigned w) NOEXCEPT
{

#if defined(AVX2OPT) || defined (AVX512OPT)
	return (unsigned)_tzcnt_u32(w);
#else
#if defined(USE_GCC_BUILD) || (defined(__GNUG__) && (defined(__arm__) || defined(__aarch64__)))
	return (unsigned)__builtin_ctz(w);
#else
	return bit_scan_forward32(w);
#endif
#endif
}


/// 64-bit bit-scan fwd
inline
unsigned count_trailing_zeros_u64(id64_t w) NOEXCEPT
{

#if defined(AVX2OPT) || defined (AVX512OPT)
	return (unsigned)_tzcnt_u64(w);
#else
#if defined(USE_GCC_BUILD) || (defined(__GNUG__) && (defined(__arm__) || defined(__aarch64__)))
	return (unsigned)__builtin_ctzll(w);
#else
	unsigned z;
	unsigned w1 = unsigned(w);
	if (!w1)
	{
		z = 32;
		w1 = unsigned(w >> 32);
		z += bit_scan_forward32(w1);
	}
	else
	{
		z = bit_scan_forward32(w1);
	}
	return z;
#endif
#endif
}

/*!
	@ingroup bitfunc
*/
template <class T>
unsigned bit_scan_reverse(T value) NOEXCEPT
{
	if (conditional<sizeof(T) == 8>::test())
	{
#if defined(USE_GCC_BUILD) || (defined(__GNUG__) && (defined(__arm__) || defined(__aarch64__)))
		return (unsigned)(63 - __builtin_clzll(value));
#else
		id64_t v8 = value;		
		unsigned v = (unsigned)(v8 >> 32);
		if (v)
		{
			return bit_scan_reverse32(v) + 32;	
		}
#endif
	}
	return bit_scan_reverse32((unsigned)value);
}

/*!

	@ingroup bitfunc
*/
template <class T>
unsigned bit_scan_forward(T value) NOEXCEPT
{
	if (conditional<sizeof(T) == 8>::test())
	{
#if defined(USE_GCC_BUILD) || (defined(__GNUG__) && (defined(__arm__) || defined(__aarch64__)))
		return (unsigned)(63 - __builtin_clzll(value));
#else
		id64_t v8 = value;
		unsigned v = (unsigned)(v8 >> 32);
		if (v)
		{
			return bit_scan_forward32(v);
		}
		return bit_scan_forward32((unsigned)v8) + 32;
		
#endif
	}
	return bit_scan_forward32((unsigned)value);
}


/*!

	@ingroup bitfunc
*/
template <class T>
unsigned count_leading_zeros_ux(T value) NOEXCEPT
{
	if (conditional<sizeof(T) == 8>::test())
	{
		return (unsigned)count_leading_zeros_u64;
	}
	return  (unsigned)count_leading_zeros_u32;
}


/*!

	@ingroup bitfunc
*/
template <class T>
unsigned count_trailing_zeros_ux(T value) NOEXCEPT
{
	if (conditional<sizeof(T) == 8>::test())
	{
		return (unsigned)count_trailing_zeros_u64;
	}
	return  (unsigned)count_trailing_zeros_u32;
}


#endif /* _SCAN_H_ */
