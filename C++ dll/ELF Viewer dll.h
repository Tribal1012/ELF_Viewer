// ELF Viewer dll.h

#pragma once

#include <iostream>
#include <cstdio>
#include <cstdlib>
#include <cstddef>
#include <cstring>

static FILE* elf;

using namespace System;

namespace ELFViewer {

	public class ELF32_header		//32bit ELF 헤더의 구조체 정보를 저장하기 위한 구조체
	{
	private:
		unsigned char magic[16];
		short type;
		short machine;
		int version;
		int entry;					//Entry Point
		int phoff;					//Program Header Offset
		int shoff;					//Section Header Offset	
		int flags;
		short ehsize;				//ELF Header 크기
		short phentsize;			//Program Header 크기
		short phnum;				//Program Header 갯수
		short shentsize;			//Section Header 크기
		short shnum;				//Section Header 갯수
		short shstrndx;

	public:
	//offsetof(ELF32_header, magic) 반환 값으로 offset 값
	/* 함수 : 출력 반환 함수
	 * 기능 : 출력을 호출할 경우 출력을 위해 지원
	 * 반환 : const int Or short
	 */
		const unsigned char* outmagic(void) const
		{
			return magic;
		}
		const short outtype(void) const
		{
		return type;
		}
		const short outmachine(void) const
		{
			return machine;
		}
		const int outversion(void) const
		{
			return version;
		}
		const int outentry(void) const
		{
			return entry;
		}
		const int outphoff(void) const
		{
			return phoff;
		}
		const int outshoff(void) const
		{
			return shoff;
		}
		const int outflags(void) const
		{
			return flags;
		}
		const short outehsize(void) const
		{
			return ehsize;
		}
		const short outphsize(void) const
		{
			return phentsize;
		}
		const short outphnum(void) const
		{
			return phnum;
		}
		const short outshsize(void) const
		{
			return shentsize;
		}
		const short outshnum(void) const
		{
			return shnum;
		}
		const short outshstrndx(void) const
		{
			return shstrndx;
		}

	/* 함수 : elf32output
	* 기능 : elf 구조체에 저장된 값을 통해 내용에 대한 정보 출력, 임시로 만든것이라 break로 대충 해둠.
	* 반환 : char*
	*/
		char* elf32output_data_endian(char endian);
		char* elf32output_file_version(char f_version);
		char* elf32output_type(short type);
		char* elf32output_machine(short machine);
		char* elf32output_version(int version);
	};

/*--------------------------------64bit ELF 헤더------------------------------------*/
	public class ELF64_header		//64bit ELF 헤더의 구조체 정보를 저장하기 위한 구조체
	{
	private:
		unsigned char magic[16];
		short type;
		short machine;
		int version;
		__int64 entry;					//Entry Point
		__int64 phoff;					//Program Header Offset
		__int64 shoff;					//Section Header Offset
		int flags;
		short ehsize;					//ELF Header 크기
		short phentsize;				//Program Header 크기
		short phnum;					//Program Header 갯수
		short shentsize;				//Section Header 크기
		short shnum;					//Section Header 갯수
		short shstrndx;

	public:
	/* 함수 : 출력 반환 함수
	 * 기능 : 출력을 호출할 경우 출력을 위해 지원
	 * 반환 : const int Or short
	 */
		const unsigned char* outmagic(void) const
		{
			return magic;
		}
		const short outtype(void) const
		{
			return type;
		}
		const short outmachine(void) const
		{
			return machine;
		}
		const int outversion(void) const
		{
			return version;
		}
		const __int64 outentry(void) const
		{
			return entry;
		}
		const __int64 outphoff(void) const
		{
			return phoff;
		}
		const __int64 outshoff(void) const
		{
			return shoff;
		}
		const int outflags(void) const
		{
			return flags;
		}
		const short outehsize(void) const
		{
			return ehsize;
		}
		const short outphsize(void) const
		{
			return phentsize;
		}
		const short outphnum(void) const
		{
			return phnum;
		}
		const short outshsize(void) const
		{
			return shentsize;
		}
		const short outshnum(void) const
		{
			return shnum;
		}
		const short outshstrndx(void) const
		{
			return shstrndx;
		}

	/* 함수 : elf64output
	 * 기능 : elf 구조체에 저장된 값을 통해 내용에 대한 정보 출력, 임시로 만든것이라 break로 대충 해둠.
	 * 반환 : char*
	 */
		char* elf64output_data_endian(char endian);
		char* elf64output_file_version(char f_version);
		char* elf64output_type(short type);
		char* elf64output_machine(short machine);
		char* elf64output_version(int version);
	};

/*--------------------------------32bit Program 헤더------------------------------------*/
	public class Program32_header	//ELF 파일의 프로그램 헤더를 저장하기 위한 구조체
	{
	private:
		int type;
		int offset;
		int vaddr;
		int paddr;
		int filesz;
		int memsz;
		int flags;
		int align;

	public:
	/* 함수 : 출력 반환 함수
	 * 기능 : 출력을 호출할 경우 출력을 위해 지원
	 * 반환 : const int Or short
	 */
		const int outtype(void) const
		{
			return type;
		}
		const int outoffset(void) const
		{
			return offset;
		}
		const int outvaddr(void) const
		{
			return vaddr;
		}
		const int outpaddr(void) const
		{
			return paddr;
		}
		const int outfilesz(void) const
		{
			return filesz;
		}
		const int outmemsz(void) const
		{
			return memsz;
		}
		const int outflags(void) const
		{
			return flags;
		}
		const int outalign(void) const
		{
			return align;
		}
	/* 함수 : program32output
	 * 기능 : program 구조체에 저장된 값을 통해 내용에 대한 정보 출력
	 * 반환 : char*
	 */
		char* program32output_type(int type);
		char* program32output_flags(int flags);
	};

/*--------------------------------64bit Program 헤더------------------------------------*/
	public class Program64_header	//ELF 파일의 프로그램 헤더를 저장하기 위한 구조체
	{
	private:
		int type;
		int flags;
		__int64 offset;
		__int64 vaddr;
		__int64 paddr;
		__int64 filesz;
		__int64 memsz;
		__int64 align;

	public:
	/* 함수 : 출력 반환 함수
	 * 기능 : 출력을 호출할 경우 출력을 위해 지원
	 * 반환 : const int Or short
	 */
		const int outtype(void) const
		{
			return type;
		}
		const int outflags(void) const
		{
			return flags;
		}
		const __int64 outoffset(void) const
		{
			return offset;
		}
		const __int64 outvaddr(void) const
		{
			return vaddr;
		}
		const __int64 outpaddr(void) const
		{
			return paddr;
		}
		const __int64 outfilesz(void) const
		{
			return filesz;
		}
		const __int64 outmemsz(void) const
		{
			return memsz;
		}
		const __int64 outalign(void) const
		{
			return align;
		}
	/* 함수 : program64output
	 * 기능 : program 구조체에 저장된 값을 통해 내용에 대한 정보 출력
	 * 반환 : char*
	 */
		char* program64output_type(int type);
		char* program64output_flags(int flags);
	};

/*--------------------------------32bit Section 헤더------------------------------------*/
	public class Section32_header	//ELF 파일의 섹션 헤더를 저장하기 위한 구조체
	{
	private:
		int name;
		int type;
		int flags;
		int addr;
		int offset;
		int size;
		int link;
		int info;
		int addralign;
		int entsize;

	public:
	/* 함수 : 출력 반환 함수
	 * 기능 : 출력을 호출할 경우 출력을 위해 지원
	 * 반환 : const int Or short
	 */
		const int outname(void) const
		{
			return name;
		}
		const int outtype(void) const
		{
			return type;
		}
		const int outflags(void) const
		{
			return flags;
		}
		const int outaddr(void) const
		{
			return addr;
		}
		const int outoffset(void) const
		{
			return offset;
		}
		const int outsize(void) const
		{
			return size;
		}
		const int outlink(void) const
		{
			return link;
		}
		const int outinfo(void) const
		{
			return info;
		}
		const int outaddralign(void) const
		{
			return addralign;
		}
		const int outentsize(void) const
		{
			return entsize;
		}
	/* 함수 : section32output
	* 기능 : 32bit section 구조체에 저장된 값을 통해 내용에 대한 정보 출력
	* 반환 : char*
	*/
		char* section32output_name(int name);
		char* section32output_type(int type);
		char* section32output_flags(int flags);
	};

/*--------------------------------64bit Section 헤더------------------------------------*/
	public class Section64_header	//ELF 파일의 섹션 헤더를 저장하기 위한 구조체
	{
	private:
		int name;
		int type;
		__int64 flags;
		__int64 addr;
		__int64 offset;
		__int64 size;
		int link;
		int info;
		__int64 addralign;
		__int64 entsize;

	public:
	/* 함수 : 출력 반환 함수
	 * 기능 : 출력을 호출할 경우 출력을 위해 지원
	* 반환 : const int Or short
	*/
		const int outname(void) const
		{
			return name;
		}
		const int outtype(void) const
		{
			return type;
		}
		const __int64 outflags(void) const
		{
			return flags;
		}
		const __int64 outaddr(void) const
		{
			return addr;
		}
		const __int64 outoffset(void) const
		{
			return offset;
		}
		const __int64 outsize(void) const
		{
			return size;
		}
		const int outlink(void) const
		{
			return link;
		}
		const int outinfo(void) const
		{
			return info;
		}
		const __int64 outaddralign(void) const
		{
			return addralign;
		}
		const __int64 outentsize(void) const
		{
			return entsize;
		}
	/* 함수 : section64output
	 * 기능 : 64bit section 구조체에 저장된 값을 통해 내용에 대한 정보 출력
	 * 반환 : char*
	 */
		char* section64output_name(int name);
		char* section64output_type(int type);
		char* section64output_flags(__int64 flags);
	};

/*-------------------------구조체 사용을 위한 공용체 선언(메모리 낭비 해결)---------------------------*/
	union ELF_header
	{
		ELF32_header e32_header;
		ELF64_header e64_header;
	};

	union Program_header
	{
		Program32_header p32_header[20];
		Program64_header p64_header[20];
	};

	union Section_header
	{
		Section32_header s32_header[40];
		Section64_header s64_header[40];
	};

/*------------------------------------상속을 위한 최상위의 부모클래스-----------------------------------*/
	public class Header
	{
/*-----------------------공용체 선언-------------------------*/
	private:
		union ELF_header e_header;
		union Program_header p_header;						//프로그램 헤더의 갯수가 여러개 이므로 배열로 선언
		union Section_header s_header;						//위와 동일, 다만 갯수가 저것 이상이 될 경우 문제 발생하므로 대책 필요

	protected:
	
	public:
/*-----------------------멤버 변수-------------------------*/
		int fsize, offset32, error, i;
		__int64 offset64;
		char* data;
		const char bitcheck;

/*-----------------------멤버 함수-------------------------*/
		Header() : fsize(0), offset32(0), offset64(0), bitcheck((char)0x01) {			//디폴트 생성자
			elf = 0x0;
		}			

	/* 함수 : fileopen
	 * 기능 : ELF 파일 바이너리 형태로 실행
	 * 반환 : int
	 */
		int fileopen(const char* filename)
		{
			fopen_s(&elf, filename, "rb");							//인자로 받은 ELF 파일 실행 
	
			if(!elf)												//파일 실행 실패, 파일 확인 필요
			{
				//에러 출력
	
				exit(1);											//에러로 인한 발생, 강제종료
			}

			fseek(elf, 0, SEEK_END);
			fsize = ftell(elf);										//fseek 함수를 통해 파일의 끝으로 이동한 후 파일의 크기 측정
			rewind(elf);											//파일의 처음 부분으로 복귀

			if((data=(char*)malloc(fsize*sizeof(char))) == 0)		//파일의 크기만큼 힙 영역 할당 받음
				//에러 출력

			error = fread_s(data,fsize,fsize,sizeof(char),elf);		//fread를 통해 파일의 이진 내용 힙 영역에 복사
			if(error == fsize)
				perror("Failed to read file.\n");

			if(memcmp(data+1, "ELF", 3))							//ELF 파일 체크(ELF앞 이상한 특수문자가 붙으므로 1칸 띄고 3글자 확인)
			{
				//에러 출력

				exit(1);											//에러로 인한 발생, 강제종료
			}

			if(!memcmp(data+4, &bitcheck, 1))		//bit 체크, 32bit일 시 바로 아래, 아닐 시 else
			{
				return 0x01;
			}
			else									//64bit
			{
				return 0x02;
			}
		}

	/* 함수 : _32input
	 * 기능 : 32bit ELF 파일 내용 구조체 메모리에 저장
	 * 반환 : void
	 */
		void _32input(void)
		{
			memcpy_s(&e_header.e32_header, sizeof(e_header.e32_header), data, sizeof(e_header.e32_header));		//32bit ELF 파일의 ELF header를 복사하여 구조체에 저장
		
			offset32 = e_header.e32_header.outphoff();
			for(i=0; i<(e_header.e32_header.outphnum()); i++)											//32bit ELF 파일의 Program header를 복사
			{
				memcpy_s(&p_header.p32_header[i], e_header.e32_header.outphsize(), (data + offset32), e_header.e32_header.outphsize());
				offset32 += e_header.e32_header.outphsize();
			}

			offset32 = e_header.e32_header.outshoff();
			for(i=0; i<(e_header.e32_header.outshnum()); i++)											//32bit ELF 파일의 Section header를 복사
			{
				memcpy_s(&s_header.s32_header[i], e_header.e32_header.outshsize(), (data + offset32), e_header.e32_header.outshsize());
				offset32 += e_header.e32_header.outshsize();
			}

			free(data);					//전체 복사 완료되었으니 힙영역 해제
		}

	/* 함수 : _64input
	 * 기능 : 64bit ELF 파일 내용 구조체 메모리에 저장
	 * 반환 : void
	 */
		void _64input(void)
		{
			memcpy_s(&e_header.e64_header, sizeof(e_header), data, sizeof(e_header));		//64bit ELF 파일의 ELF header를 복사하여 구조체에 저장

			offset64 = e_header.e64_header.outphoff();
			for(i=0; i<(e_header.e64_header.outphnum()); i++)
			{
				memcpy_s(&p_header.p64_header[i], e_header.e64_header.outphsize(), (data + offset64), e_header.e64_header.outphsize());
				offset64 += e_header.e64_header.outphsize();
			}

			offset64 = e_header.e64_header.outshoff();
			for(i=0; i<(e_header.e64_header.outshnum()); i++)
			{
				memcpy_s(&s_header.s64_header[i], e_header.e64_header.outshsize(), (data + offset64), e_header.e64_header.outshsize());
				offset64 += e_header.e64_header.outshsize();
			}

			free(data);					//전체 복사 완료되었으니 힙영역 해제
		}

		~Header()													//프로그램 종료를 나타내므로 파일 종료를 위한 소멸자
		{
			try														//elf 파일이 실행되지 않았을 경우 elf
			{
				if(elf == NULL)
					throw -1;
				fclose(elf);
			}
			catch(int exception)
			{
				exception = 0;
	
				exit(0);
			}
		}
	};
}
